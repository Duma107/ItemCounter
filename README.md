# Advanced Interactive Item Counter

**Author:** Dumisani Nxumalo

## Description

A comprehensive C# console application that counts occurrences of items across multiple data types. Features an interactive menu system supporting strings, integers, doubles, characters, booleans, and dates with intelligent parsing and real-time user input.

## Features

- **Multi-Type Data Support**: Handles 6 different data types seamlessly
- **Interactive Menu System**: Intuitive numbered menu for easy navigation
- **Intelligent Input Parsing**: Smart conversion with comprehensive error handling
- **Flexible Boolean Input**: Accepts multiple boolean representations
- **Date Format Support**: Handles common date formats with validation
- **Generic LINQ-Powered Engine**: Efficient counting using modern C# features
- **Continuous Operation**: Loop-based interface until user exit
- **Robust Error Handling**: Clear feedback for invalid inputs and formats

## Supported Data Types

| Type | Description | Input Examples |
|------|-------------|----------------|
| **Strings/Words** | Text separated by spaces | `apple banana apple orange` |
| **Integers** | Whole numbers | `1 2 3 2 1 4 5` |
| **Doubles** | Decimal numbers | `3.14 2.5 3.14 1.0 2.7` |
| **Characters** | Individual characters | `hello world` |
| **Booleans** | True/false values | `true false yes no 1 0` |
| **Dates** | Date values | `01/15/2024 2024-12-25 03/10/2023` |

## Requirements

- .NET SDK (5.0 or higher recommended)
- C# compiler
- Console/Terminal access

## Installation

1. Clone or download the project files
2. Navigate to the project directory
3. Ensure you have .NET SDK installed

```bash
# Verify .NET installation
dotnet --version
```

## Usage

### Running the Program

```bash
# Build the project
dotnet build

# Run the interactive program
dotnet run
```

### Alternative Compilation

```bash
# Compile using C# compiler
csc Program.cs

# Run the executable
Program.exe  # Windows
mono Program.exe  # Linux/Mac
```

## User Interface

The program presents a comprehensive menu system:

```
=== Item Counter ===
1. Count words/strings
2. Count integers
3. Count doubles
4. Count characters
5. Count booleans
6. Count dates
7. Exit
Choose an option (1-7):
```

## Detailed Usage Examples

### Option 1: Count Words/Strings
```
Enter words/strings separated by spaces:
apple banana apple orange banana apple grape

Counting strings:
apple: 3 occurrence(s)
banana: 2 occurrence(s)
orange: 1 occurrence(s)
grape: 1 occurrence(s)
```

### Option 2: Count Integers
```
Enter integers separated by spaces:
10 20 10 30 20 10 40

Counting integers:
10: 3 occurrence(s)
20: 2 occurrence(s)
30: 1 occurrence(s)
40: 1 occurrence(s)
```

### Option 3: Count Doubles
```
Enter decimal numbers separated by spaces:
3.14 2.5 3.14 1.0 2.5 4.7

Counting doubles:
3.14: 2 occurrence(s)
2.5: 2 occurrence(s)
1: 1 occurrence(s)
4.7: 1 occurrence(s)
```

### Option 4: Count Characters
```
Enter text (each character will be counted):
programming

Counting characters:
p: 1 occurrence(s)
r: 2 occurrence(s)
o: 1 occurrence(s)
g: 2 occurrence(s)
a: 1 occurrence(s)
m: 2 occurrence(s)
i: 1 occurrence(s)
n: 2 occurrence(s)
```

### Option 5: Count Booleans
```
Enter boolean values separated by spaces (true/false, yes/no, 1/0):
true false true yes no 1 0 true

Counting booleans:
True: 4 occurrence(s)
False: 4 occurrence(s)
```

### Option 6: Count Dates
```
Enter dates separated by spaces (format: MM/dd/yyyy or yyyy-MM-dd):
01/15/2024 2024-12-25 01/15/2024 03/10/2023 2024-12-25

Counting dates:
1/15/2024 12:00:00 AM: 2 occurrence(s)
12/25/2024 12:00:00 AM: 2 occurrence(s)
3/10/2023 12:00:00 AM: 1 occurrence(s)
```

## Technical Architecture

### Core Components

- **Generic Counter Method**: Universal `CountOccurrences<T>` using LINQ
- **Type-Specific Parsers**: Dedicated parsing methods for each data type
- **Menu System**: Switch-based navigation with validation
- **Error Handling**: Try-catch blocks with specific error messages

### Data Processing Flow

1. **Menu Selection**: User chooses data type
2. **Input Collection**: Program prompts for data entry
3. **Input Validation**: Checks for empty/null input
4. **Type Conversion**: Parses string input to target type
5. **LINQ Processing**: Groups and counts using `GroupBy()` and `Select()`
6. **Result Display**: Formatted output with occurrence counts
7. **Loop Continuation**: Returns to menu for next operation

### Boolean Parsing Logic

The program accepts flexible boolean input:

```csharp
"true" or "yes" or "1" → true
"false" or "no" or "0" → false
```

### Date Format Support

Accepts multiple date formats:
- **MM/dd/yyyy**: American format (01/15/2024)
- **yyyy-MM-dd**: ISO format (2024-12-25)
- **Automatic Detection**: Uses DateTime.Parse() for flexibility

## Error Handling

| Error Type | Handling | User Feedback |
|------------|----------|---------------|
| **Empty Input** | Graceful return | "No input provided." |
| **Invalid Integers** | FormatException catch | "Invalid integer format..." |
| **Invalid Doubles** | FormatException catch | "Invalid decimal format..." |
| **Invalid Booleans** | Custom validation | "'{value}' is not a valid boolean..." |
| **Invalid Dates** | FormatException catch | "Invalid date format. Please use..." |
| **Invalid Menu** | Default case | "Invalid option. Please try again." |

## Performance Features

- **LINQ Optimization**: Efficient grouping and counting
- **Memory Management**: Processes data in single pass
- **Type Safety**: Generic constraints prevent runtime errors
- **Lazy Evaluation**: LINQ deferred execution for performance

## Code Quality

- **Single Responsibility**: Each method handles one data type
- **DRY Principle**: Generic counter method reused across types
- **Error Boundaries**: Isolated error handling per operation
- **Clean Code**: Readable variable names and clear structure

## Project Structure

```
ItemCounter/
├── Program.cs          # Main application with all functionality
├── README.md          # This documentation
└── ItemCounter.csproj # Project configuration (if using dotnet)
```

## Future Enhancements

- **File Input/Output**: Read from and write results to files
- **Custom Delimiters**: Support comma, semicolon, or custom separators
- **Case Sensitivity Options**: Toggle for string comparisons
- **Statistical Analysis**: Mean, median, mode calculations
- **Export Formats**: JSON, CSV, XML output options
- **Batch Processing**: Process multiple datasets in sequence
- **Configuration File**: Save user preferences
- **GUI Version**: Windows Forms or WPF interface

## Technical Specifications

- **Language**: C# (.NET 5.0+)
- **Paradigm**: Object-oriented with functional LINQ elements
- **Memory Usage**: Efficient single-pass processing
- **Performance**: O(n) time complexity for counting operations
- **Compatibility**: Cross-platform (.NET Core/5+)

## Use Cases

- **Data Analysis**: Quick frequency analysis of datasets
- **Quality Assurance**: Validate data consistency
- **Educational**: Learn about data types and LINQ
- **Debugging**: Analyze data patterns and duplicates
- **Research**: Statistical analysis of categorical data

## Author

**Dumisani Nxumalo**  
*Software Developer*

## License

This project is for educational and demonstration purposes.

## Version History

- **v2.0**: Added doubles, booleans, and dates support
- **v1.1**: Enhanced user interface and error handling
- **v1.0**: Initial release with strings, integers, and characters

---

*Last updated: May 2025*
