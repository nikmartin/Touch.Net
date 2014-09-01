'''#################################################
''' Touch.Net
''' .Net version of the nix touch utility
''' 2005 Nik Martin
''' touch@nik-martin.com
''' 
''' This is the MIT License, with one addition:
''' I want attribution in source code form, which means any code of mine you use,
''' you must keep this entire license intact, including the next line:
'''
''' Nik Martin wrote the original version of this software.  
''' http://www.nikmartin.com
'''
'''
''' Copyright (c) <year> Nik Martin
'''
''' Permission is hereby granted, free of charge, to any person obtaining a copy of 
''' this software and associated documentation files (the "Software"), to deal in 
''' the Software without restriction, including without limitation the rights to 
''' use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
''' the Software, and to permit persons to whom the Software is furnished to do so, 
''' subject to the following conditions:
'''
''' The above copyright notice and this permission notice shall be included in all 
''' copies or substantial portions of the Software.
'''
''' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
''' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
''' FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
''' COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
''' IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
''' CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
''' #################################################


Imports System
Imports System.IO
Imports System.Collections

' Takes an array of file names or directory names on the command line.  
' Determines what kind of name it is and processes it appropriately
Public Class Touch
    
    shared dtModDate As DateTime=system.DateTime.Now
    
    'Entry point which delegates to C-style main function
    Public Overloads Shared Sub Main()
        Main(System.Environment.GetCommandLineArgs(1))
    End Sub
    
	Private overloads  Shared Sub Main(args As String)
        Dim path As String=args
        'For Each path In  args
            If File.Exists(path) Then
                ' This path is a file
                ProcessFile(path)
            Else
                If Directory.Exists(path) Then
                    ' This path is a directory
                    ProcessDirectory(path)
                Else
                    Console.WriteLine("{0} is not a valid file or directory.", path)
                End If
            End If
        'Next path
    End Sub 'Main

    
    ' Process all files in the directory passed in, and recurse on any directories 
    ' that are found to process the files they contain
    Public Shared Sub ProcessDirectory(targetDirectory As String)
        
        Dim fileName As String
        Dim fileEntries As String() = Directory.GetFiles(targetDirectory)
                       
        
        ' Process the list of files found in the directory
              
        
        For Each fileName In  fileEntries
            ProcessFile(fileName)
        Next fileName
       
        Dim subdirectoryEntries As String() = Directory.GetDirectories(targetDirectory)
        ' Recurse into subdirectories of this directory
        Dim subdirectory As String
        For Each subdirectory In  subdirectoryEntries
            ProcessDirectory(subdirectory)
        Next subdirectory
    
    End Sub 'ProcessDirectory
    
    Public Shared Sub ProcessFile(path As String)
    	
    	Dim fTouch As File
    	Dim wasModified As Boolean= true
    	
    	Try
    		
    	
    	fTouch.SetLastWriteTime(path,dtModDate)
    	
    Catch e As system.io.IOException
     
     wasmodified = false
     Console.WriteLine("Did not process file '{0}'.", path & environment.NewLine  & e.message)
     
          
    End Try
    
       If wasmodified 	Then
       	
      Console.WriteLine("Processed file '{0}'.", path)
      
      End If
       
       
    	
    End Sub 'ProcessFile
End Class 'RecursiveFileProcessor
