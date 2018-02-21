use std::path::{PathBuf};
use std::fs::{read_dir, File, OpenOptions};
use std::io::{BufReader, BufRead, Write};

pub mod parser;
mod c_sharp;

fn main() {
    let folder = PathBuf::from("input");
    let dir = read_dir(folder).expect("Unable to open input folder");
    let mut strings: Vec<String> = vec!();
    for file in dir {
        match file {
            Ok(f) => {
                strings.push(parse_file(f.path()));
            },
            Err(e) => {
                println!("Unable to read file path {:?}", e);
            }
        }
    }
    let mut f = OpenOptions::new()
                    .write(true)
                    .create(true)
                    .open("index.d.ts")
                    .expect("Cannot open output file");
    for string in strings {
       let _ = f.write_all(string.as_bytes());
    }
}

fn parse_file(file_name: PathBuf) -> String {
    let f = File::open(file_name).expect("Unable to open file");
    let reader = BufReader::new(f);
    convert_to_string(reader, c_sharp::CSharpParser::default())
}

fn convert_to_string<T>(reader: BufReader<File>, p: T) -> String
where T: parser::Parser {
    p.parse(reader)
}