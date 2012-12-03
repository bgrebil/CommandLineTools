A set of simple command line tools.

### CreateGuid

    cguid [-f=<format>] [-o=<file>] [-n=#]

    Options:
    --------
    -f    GUID string format. Values match .NET format specifiers (N, D, B, P). Default = B.
    -o    Write output to named file. When not specified, output is copied to clipboard.
    -n    Create n GUIDs.

    Examples:
    ---------
    cguid                      - Creates a single GUID copied to the clipboard
    cguid -n=5                 - Creates 5 GUIDs copied to the clipboard
    cguid -n=20 -o=output.txt  - Creates 20 GUIDs written to the output.txt file
    
### DeleteFiles
Created from https://github.com/RickStrahl/DeleteFiles

    delfiles -r -f -y -d=# -s=# <pathspec>
    
    Options:
    --------
    pathSpec    Path and File Spec. Make sure to add a filespec
    -r          Delete files [R]ecursively     
    -f          Remove empty [F]olders
    -y          Delete to Rec[Y]le Bin (can be slow!)
    -d=XX       Number of [D]ays before the current date to delete            
    -s=XX       Number of [S]econds before the current time to delete
    
    Seconds override days
    If neither -d or -s no date filter is applied
    
    Examples:
    ---------
    delfiles -r -f c:\temp\*.*         - deletes all files in temp folder recursively 
                                         and deletes empty folders
    delfiles -r -f -d=10 c:\temp\*.*    - delete files 10 days or older 
    delfiles -r -f -s=3600 c:\temp\*.*  - delete files older than an hour
    delfiles -r ""c:\My Files\*.*""      - deletes all files in temp folder recursively
    
### Which

    which <file.exe>[, <file2.exe>, ...]
    
    Options:
    --------
    file  - executable to search for in the PATH environment variable

### XmlFormatter

    xf [-i=3] [-o=<output>] [-c] input.xml
    
    Options:
    --------
    input    - The XML file to format
    -o       - Specifies the output file, if not specified text is dumped to console
    -i       - Specifies the spacing to use on each indent level. Default = 3.
    -c       - Read the input from the clipboard instead of a file
    
### XmlSchemaValidator

    xsv input.xml schema.xsd
    
    Options:
    --------
    input    - The xml to validate
    schema   - The schema to validate against
    
    
### XPathQuery

    xpq <input> <xpath>
    
    Options:
    --------
    input    - the input xml file
    xpath    - the xpath expression to query