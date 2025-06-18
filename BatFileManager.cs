using System;
using System.Collections.Generic;
using System.IO;

public class BatFile
{
    public string FilePath { get; }
    public string FileName => Path.GetFileName(FilePath);
    public List<IAction> Actions { get; }

    public BatFile(string filePath, List<IAction> actions)
    {
        FilePath = filePath;
        Actions = new List<IAction>(actions);
    }

    public override string ToString() => FileName;
    public override bool Equals(object obj)
    {
        if (obj is BatFile other)
            return FilePath.Equals(other.FilePath, StringComparison.OrdinalIgnoreCase);
        return false;
    }
    public override int GetHashCode()
    {
        return FilePath.GetHashCode();
    }
}

public class BatFileManager
{
    private readonly List<BatFile> _batFiles = new List<BatFile>();

    public List<BatFile> GetBatFiles() => new List<BatFile>(_batFiles);

    public BatFile CreateBatFile(List<IAction> actions, string fileName)
    {
        if (actions == null || actions.Count == 0)
            throw new ArgumentException("Нет действий для создания батника");
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Имя файла не указано");

        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c.ToString(), "");
        if (!fileName.EndsWith(".bat", StringComparison.OrdinalIgnoreCase))
            fileName += ".bat";

        var content = new System.Text.StringBuilder();
        foreach (var action in actions)
            content.AppendLine(action.ToBatCommand());

        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folder = System.IO.Path.Combine(desktop, "BatFiles");
        if (!System.IO.Directory.Exists(folder))
            System.IO.Directory.CreateDirectory(folder);

        string filePath = System.IO.Path.Combine(folder, fileName);

        System.IO.File.WriteAllText(filePath, content.ToString());
        var batFile = new BatFile(filePath, actions);
        _batFiles.Add(batFile);
        return batFile;
    }

    public void RunBatFile(BatFile batFile)
    {
        if (!File.Exists(batFile.FilePath))
            throw new FileNotFoundException("Файл не найден", batFile.FilePath);
        System.Diagnostics.Process.Start(batFile.FilePath);
    }
}
