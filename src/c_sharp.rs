use std::io::{BufReader,BufRead};
use std::fs::File;
use super::parser;

pub struct CSharpConfig {
    pub use_concrete: bool,
    
}

impl CSharpConfig {
    fn default() -> Self {
        CSharpConfig {
            use_concrete: false,
        }
    }
}
pub struct CSharpParser {
    pub config: CSharpConfig,
}

impl parser::Parser for CSharpParser {
    fn parse(&self, reader: BufReader<File>) -> String {
        let mut classes: Vec<(Class)> = vec!();
        for line in reader.lines() {
            match self.check_line(line.expect("Unable to read line")) {
                Some(prop) => {
                    match prop.data_type {
                        DataType::Class => {
                            classes.push(
                                Class {
                                    name: prop.name,
                                    props: vec!(),
                                    is_enum: false,
                                }
                            );
                        },
                        DataType::Enum => {
                        classes.push(
                            Class {
                                name: prop.name,
                                props: vec!(),
                                is_enum: true,
                            }
                        );
                        },
                        _ => {
                            let i = classes.len() - 1;
                            classes[i].props.push(prop);
                        }
                    }
                },
                None => (),
            }
        }
        self.convert_to_string(classes)
    }
}



impl CSharpParser {
    
    pub fn default() -> Self {
        CSharpParser {
            config: CSharpConfig::default(),
        }
    }

    fn convert_to_string(&self, classes: Vec<Class>) -> String {
        let mut ret = String::new();
        for class in classes {
            ret += &class.to_string();
        }
        ret
    }
    fn check_line(&self, line: String) -> Option<Prop> {
        let line = line.trim();
        if let Some(i) = line.find("public") {
            if line.ends_with("{ }") {
                None
            } else {
                Some(self.check_public_line(String::from(line.split_at(i + 7).1.replace("static", "").trim())))
            }
        } else if line.ends_with(",") {
            self.check_for_enum_line(line)
        } else {
            None
        }
    }

    fn check_for_enum_line(&self, line: &str) -> Option<Prop> {
        if let Some(_i) = line.find("(") {
            None
        } else {
            Some(self.parse_enum_line(line))
        }
    }

    fn parse_enum_line(&self, line: &str) -> Prop {

        let (name, dt) = if let Some(i) = line.find(" ") {
            let parts = line.split_at(i);
            let val: Option<u32> = match parts.1.trim_matches('=').trim().parse() {
                Ok(u) => Some(u),
                _ => None,
            };
            let name = parts.0.trim();
            let dt = DataType::Case(val);
            (name, dt)
        } else {
            (line.trim_matches(','), DataType::Case(None))
        };
        Prop {
            name: String::from(name),
            data_type: dt,
            optional: false,
        }
    }

    fn check_public_line(&self, line: String) -> Prop {
        if line.ends_with(")") || line.ends_with("{") {
            self.parse_function(line)
        } else {
            self.parse_plain_data(line)
        }
    }

    fn parse_plain_data(&self, line: String) -> Prop {
        let mut prop_buf = line.split_whitespace();
        let mut data_type = prop_buf.next().expect("unable to unwrap data type");
        let name = prop_buf.next().expect("unable to unwrap name");
        let optional = if data_type.ends_with("?") {
            data_type = data_type.trim_matches('?');
            true
        } else {
            false
        };
        Prop {
            name: String::from(name),
            data_type: DataType::from_plain(data_type),
            optional
        }
    }

    fn parse_function(&self, line: String) -> Prop {
        let arg_start = line.find("(");
        let first_space = line.find(" ");
        
        if arg_start < first_space || first_space == None {
            let parts = line.split_at(arg_start.expect("no arg start..."));
            let ret = parts.0;
            let args = self.parse_function_args(parts.1.trim_matches(|c| c == '(' || c == ')').to_string());
            Prop {
                name: String::from("constructor"),
                data_type: DataType::from_func("void", args),
                optional: false
            }
        } else {
            let first_split = line.split_at(first_space.expect("no first space..."));
            let (ret, optional) = if first_split.0.ends_with("?") {
                (first_split.0.trim_matches('?'), true)
            } else {
                (first_split.0, false)
            };
            
            let second_split = first_split.1.split_at(arg_start.expect("no arg start..."));
            let name = String::from(second_split.0);
            let args = self.parse_function_args(second_split.1.trim_matches(|c| c == '(' || c == ')').to_string());
            Prop {
                name,
                data_type: DataType::from_func(ret, args),
                optional,
            }   
        }
    }

    fn parse_function_args(&self, line: String) -> Vec<Prop> {
        if line == "" {
            return vec!();
        };
        line
            .split(", ")
            .map(|a| { 
                self.parse_plain_data(a.to_string())
            })
            .collect::<Vec<Prop>>()
    }
}


#[derive(Debug, Clone)]
struct Class {
    name: String,
    props: Vec<Prop>,
    is_enum: bool,
}

impl Class {
    fn to_string(self) -> String {
        let mut ret = String::from("export ");
        let (entity_type, terminator) = if self.is_enum {
            ("enum", ",")
        } else {
            ("interface", ";")
        };
        ret += entity_type;
        ret += " ";
        ret += &self.name;
        ret += " {\n";
        for prop in self.props {
            match prop.data_type {
                DataType::Function(_ret, _args) => (),
                _ => {
                    ret += "    ";
                    ret += &prop.to_string();
                    ret += terminator;
                    ret += "\n"
                }
            }
        }
        ret += "}\n";
        ret
    }
}

#[derive(Debug, Clone, PartialEq)]
struct Prop {
    name: String,
    data_type: DataType,
    optional: bool,
}

impl Prop {
    fn to_string(self) -> String {
        match self.data_type {
            DataType::Case(_) => {
                let val_str = self.data_type.to_string();
                let postfix: String = if val_str.len() > 0 {
                    let mut r = String::from(" = ");
                    r += &val_str;
                    r + ","
                } else { 
                    String::new() 
                };
                let mut ret = String::new();
                ret += &self.name;
                ret + &postfix
            },
            _ => {
                let mut ret = String::new();
                ret += &self.name;
                if self.optional {
                    ret += "?";
                }
                ret += ": ";
                ret + &self.data_type.to_string()
            }
        }
    }
}

#[derive(Debug, Clone, PartialEq)]
enum DataType {
    Class,
    Enum,
    Case(Option<u32>),
    Bool,
    Number,
    String,
    Date,
    Void,
    Object(String),
    Array(Box<DataType>),
    Function(Box<DataType>, Vec<Prop>),
}

impl DataType {
    fn from_plain(name: &str) -> DataType {
        if let Some(i) = name.find("List<") {
            let inner_str = name.split_at(i + 5).1.trim_matches('>');
            DataType::Array(Box::new(DataType::from_plain(inner_str)))
        } else {
            match name {
                "class" => DataType::Class,
                "enum" => DataType::Enum,
                "bool" => DataType::Bool,
                "string" => DataType::String,
                "Guid" => DataType::String,
                "char" => DataType::String,
                "decimal" => DataType::Number,
                "double" => DataType::Number,
                "single" => DataType::Number,
                "float" => DataType::Number,
                "int" => DataType::Number,
                "void" => DataType::Void,
                "DateTime" => DataType::Date,
                _ => DataType::Object(name.to_string()),
            }
        }
    }

    fn from_func(ret: &str, args: Vec<Prop>) -> DataType {
        DataType::Function(Box::new(DataType::from_plain(ret)), args)
    }

    fn to_string(self) -> String {
        match self {
            DataType::Class => String::from("class"),
            DataType::Enum => String::from("enum"),
            DataType::Bool => String::from("boolean"),
            DataType::String => String::from("string"),
            DataType::Number => String::from("number"),
            DataType::Void => String::from("void"),
            DataType::Date => String::from("Date"),
            DataType::Object(name) => name,
            DataType::Case(val) => {
                if let Some(v) = val {
                    v.to_string()
                } else {
                    String::new()
                }
            },
            DataType::Function(ret, args) => {
                let mut r = String::from("(");
                let mut arg_string = String::new();
                for arg in args.into_iter().map(|a| a.to_string()) {
                    arg_string += &arg;
                    arg_string += ", ";
                }
                r += &arg_string;
                r += ") => ";
                r + &ret.to_string()
            },
            DataType::Array(dt) => {
                let mut r = String::from("Array<");
                r += &dt.to_string();
                r + ">"
            },
        }
    }
}
