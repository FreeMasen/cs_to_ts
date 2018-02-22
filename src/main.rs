use std::path::{PathBuf};
use std::fs::{read_dir, File, OpenOptions};
use std::io::{BufReader, Write};

extern crate clap;
use clap::{App, Arg, ArgMatches};

pub mod parser;
use parser::Parser;
mod c_sharp;
use c_sharp::{CSharpConfig, CSharpParser};

fn main() {
    let matches = get_args();
    let input = matches.value_of("input").expect("input is required but not found");
    let folder = PathBuf::from(input);
    let dir = read_dir(folder).expect("Unable to open input folder");
    let mut strings: Vec<String> = vec!();
    let cs_parser = CSharpParser {
        config: CSharpConfig {
            use_concrete: matches.is_present("concrete")
        }
    };
    for file in dir {
        match file {
            Ok(f) => {
                let path = f.path();
                let path2 = path.clone();
                let ext = path2.extension().expect("Unable to parse file w/o an extension");
                let file = File::open(path).expect("Unable to open file");
                let reader = BufReader::new(file);
                let parsed = if ext == "cs" {
                    cs_parser.parse(reader)
                } else {
                    String::new()
                };
                strings.push(parsed);
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

fn get_args() -> ArgMatches<'static> {
    App::new("to_ts")
        .version("0.1.0")
        .author("Robert Masen")
        .about("Parses a server side code file into a typescript file")
        .args(&[
            Arg::with_name("input")
            .short("i")
            .required(true)
            .multiple(false)
            .help("Input folder or file to convert")
            .takes_value(true)
            .value_name("INPUT"),
            Arg::with_name("output")
                .short("o")
                .required(true)
                .multiple(false)
                .help("Output path, if no file is provided will be saved in index.d.ts")
                .takes_value(true),
            Arg::with_name("concrete")
                .short("c")
                .required(false)
                .multiple(false)
                .help("If passed, will generate classes instead of interfaces")
                .takes_value(false),
            Arg::with_name("funcs")
                .short("f")
                .required(false)
                .multiple(false)
                .help("If passed, will parse all functions as well as properties (requires concrete")
                .requires("concrete")
                .takes_value(false),
        ]).get_matches()
}