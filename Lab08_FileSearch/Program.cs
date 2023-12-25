using System.IO.Compression;
using System.Text;

Console.Write("Enter filename: ");
string filename = Console.ReadLine()!;
Console.Write("Enter the path: ");
string path = Console.ReadLine()!;
FileStream? fstream = null; //переменная fstream для открытия файла
if (Directory.Exists(path))
{
    if (Directory.GetFiles(path).Contains(path + "\\" + filename)) //проверка на exist
    {
        fstream = File.OpenRead(path + "\\" + filename); //если exist, то открываем и присваеваем значение
    }
    else
    {
        foreach (var subdirectory in Directory.GetDirectories(path)) //перебор всех подкаталогов в данном каталоге
        {
            string newPath = new DirectoryInfo(subdirectory).FullName; //переменная, содержащая путь к полному каталогу
            if (Directory.GetFiles(newPath).Contains(newPath + "\\" + filename))
            {
                fstream = File.OpenRead(newPath + "\\" + filename);
                break;
            }
        }
    }

    byte[] buffer = new byte[fstream!.Length];//массив байтов buffer, размер которого равен длине открытого файла. ! для обозначения, что fstream не NULL
    fstream.Read(buffer, 0, buffer.Length); //считываем содержимое в массив buffer
    string textFromFile = Encoding.Default.GetString(buffer); //декодирует содержимое в строку
    Console.WriteLine(textFromFile); //вывод содержимого
    //
    FileInfo file = new FileInfo(filename);
    /// Compress(file);
    CompressFile("MEGA.txt", "MEGA.txt.gzip");
    DecompressFile("MEGA.txt.gzip");
    CreateZipFile("./arch/", "./MEGA.zip/");
}

void CreateZipFile(string sourceDirectory, string zipFile)
{
    InitSampleFilesForZip(sourceDirectory);  // создание некоторых файлов, если директория не существует
    string? destDirectory = Path.GetDirectoryName(zipFile);
    /*if (destDirectory is not null && !Directory.Exists(destDirectory))
    {
        Directory.CreateDirectory(destDirectory);
    }*/
    FileStream zipStream = File.Create(zipFile);
    using ZipArchive archive = new(zipStream, ZipArchiveMode.Create);

    IEnumerable<string> files = Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
    foreach (var file in files)
    {
        ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
        using FileStream inputStream = File.OpenRead(file);
        using Stream outputStream = entry.Open();
        inputStream.CopyTo(outputStream);
    }
}
void InitSampleFilesForZip(string directory)
{
    if (!Directory.Exists(directory))
    {
        Directory.CreateDirectory(directory);

        for (int i = 0; i < 10; i++)
        {
            string destFileName = Path.Combine(directory, $"test{i}.txt");

            File.Copy("file.txt", destFileName);
        }

    } // если нечего делать, то используем текущий путь
}
void DecompressFile(string fileName)
{
    FileStream inputStream = File.OpenRead(fileName);
    using MemoryStream outputStream = new();
    using DeflateStream compressStream = new(inputStream, CompressionMode.Decompress);
    compressStream.CopyTo(outputStream);
    outputStream.Seek(0, SeekOrigin.Begin);
    using StreamReader reader = new(outputStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 4096, leaveOpen: true);
    string result = reader.ReadToEnd();
    Console.WriteLine(result);
}

void CompressFile(string fileName, string compressedFileName)
{
    using FileStream inputStream = File.OpenRead(fileName);
    FileStream outputStream = File.OpenWrite(compressedFileName);
    using DeflateStream compressStream = new(outputStream, CompressionMode.Compress);
    inputStream.CopyTo(compressStream);//поток цепи
}