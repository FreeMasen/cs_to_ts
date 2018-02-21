use std::io::BufReader;
use std::fs::File;
pub trait Parser {
    fn parse(&self, reader: BufReader<File>) -> String;
}