use std::path::{PathBuf};
use std::fs::{read_dir, File};
use std::io::{BufReader, Read, BufRead};

fn main() {
    let folder = PathBuf::from("input");
    let dir = read_dir(folder).expect("Unable to open input folder");
    for file in dir {
        match file {
            Ok(f) => {
                parse_file(f.path());
            },
            Err(e) => {
                println!("Unalbe to read file path");
            }
        }
    }
}

fn parse_file(file_name: PathBuf) {
    let f = File::open(file_name).expect("Unable to open file");
    let reader = BufReader::new(f);
    for line in reader.lines() {
        let _ = check_line(line.expect("Unable to read line"));
    }
}

// fn check_for_class_line(line: String) -> Option<String> {
//     match line.find("class") {
//             Some(i) => {
//                 Some(String::from(line.split_at(i + 6).1)) //class` ` = 6 chars
//             },
//             None => {
//                 println!("did not find class keyword");
//                 None
//             }
//         }
// }

fn check_line(line: String) {
    match line.find("public") {
        Some(i) => {
            println!("public!\n\t{:?}", &line);
            check_public_line(String::from(line.split_at(i + 7).1));
        },
        None => println!("not public")
    }
}

fn check_public_line(line: String) {
    if line.ends_with(")") {return;}
    let mut propBuf = line.split_whitespace();
    let data_type = propBuf.next().expect("unable to unwrap data type");
    let mut name = propBuf.next().expect("unable to unwrap name");
    let optional = if data_type.ends_with("?") {
        name = name.trim_matches('?');
        true
    } else {
        false
    };
    let prop = Prop {
        name: String::from(name),
        data_type: DataType::from(data_type),
        optional
    };
    println!("prop: {:?}", prop);
}

#[derive(Debug)]
struct Prop {
    name: String,
    data_type: DataType,
    optional: bool,
}

#[derive(Debug)]
enum DataType {
    Class,
    Number,
    String,
    Object,
    Date,
    Function,
}

impl DataType {
    fn from(name: &str) -> DataType {
        match name {
            "class" => DataType::Class,
            "string" => DataType::String,
            "decimal" => DataType::Number,
            "double" => DataType::Number,
            "single" => DataType::Number,
            "float" => DataType::Number,
            "int" => DataType::Number,
            "DateTime" => DataType::Date,
            "()" => DataType::Function,
            _ => DataType::Object
        }
    }

    fn to_string(&self) -> String {
        match self {
            &DataType::Class => String::from("class"),
            &DataType::String => String::from("string"),
            &DataType::Number => String::from("number"),
            &DataType::Date => String::from("Date"),
            &DataType::Function => String::from("function"),
            _ => String::from("any"),
        }
    }
}

// enum Prop {
//     Class(String),
//     Number(String, bool),
//     String(String),
//     Object(String, String),
//     Date(String),
// }