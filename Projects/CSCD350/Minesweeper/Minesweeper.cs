
using System.IO;
using System.Collections;

/* This program wants to take an Input file with 
arbitrary data, interpret it, and spit out 
proximity to mine output. This output can be piped 
to a output file or the console */
namespace Minesweeper
{
    public class Minesweeper
    {
        private string InputFile;
        private string OutputFile;

        private class Minefield
        {
            string name;
            char[][] field;
            int height, width;

            public Minefield(int height, int width)
            {
                this.height = height;
                this.width = width;
                field = new char[height][width];
            }

            public Minefield(string name, int height, int width)
            {
                setName(name);
                this.height = height;
                this.width = width;
                field = new char[height][width];
            }

            public void writeCharToField(char input, int row, int col)
            {
                this.field[row][col] = input;
            }

            public void setName(string name)
            {
                if (name is typeof(string))
                {
                    this.name = name;
                }
            }
        }

        public Minesweeper(string inputFile, string outputFile)
        {
            readFile(inputFile);
            writeFile(outputFile);
        }

        public ArrayList readFile(string filename)
        {
            ArrayList minefieldAra = new ArrayList();
            if (setInputFile(filename))
            {
                try
                {
                    using (StreamReader input = new StreamReader(this.InputFile))
                    {
                        while (input.Peek() > -1)
                        {
                            int fieldCount = 1;
                            if (input.Read() is typeof(int))
                            {
                                height = input.Read();
                                width = input.Read();
                                minefieldAra.Add(new Minefield("Field#" + fieldCount, height, width));
                            }
                            else if (input.Read() is typeof(char))
                            {
                                for (int row = 0; row < height; row++)
                                {
                                    for (int col = 0; col < width; col++)
                                    {
                                        Minefield field = minefieldAra.IndexOf(fieldCount - 1);
                                        field.writeCharToField((char)input.Read(), row, col);
                                    }
                                }
                            }
                            else
                            {
                                input.ReadLine();
                            }
                            fieldCount++;
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Unexpected error reading file: " + filename + "\nERROR: " + e.Message);
                }
                return minefieldAra;
            }
            else
            {
                System.Console.WriteLine("Input file was unable to be set. Filename: " + filename);
            }
        }

        public void writeFile(string outputFile)
        {
            if (File.Exists(outputFile))
            {
                System.Console.WriteLine("File already exists.");
            }
            else
            {
                File.Create(outputFile);
                File.Open(outputFile, FileMode.Append);
            }
        }

        public bool setInputFile(string filename)
        {
            if (File.Exists(filename))
            {
                this.InputFile = filename;
                return true;
            }
            else
            {
                System.Console.WriteLine("File does not exist." + filename);
                return false;
            }
        }
    }
}
