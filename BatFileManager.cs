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

    // ��������� ���� ��� �������� ���� � ����� BatFiles
    private readonly string _folder; // *** ��������� ***

    // ����������� � ��������� �������� �� �����
    public BatFileManager()
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        _folder = Path.Combine(desktop, "BatFiles"); // *** ��������� ***
        LoadBatFilesFromFolder(); // *** ��������� ***
    }

    // ����� ����� ��� �������� �������� �� �����
    private void LoadBatFilesFromFolder() // *** ��������� ***
    {
        if (!Directory.Exists(_folder))
            Directory.CreateDirectory(_folder);

        var batFilePaths = Directory.GetFiles(_folder, "*.bat");
        foreach (var path in batFilePaths)
        {
            // ������ BatFile � ������ ������� �������� (����� ���������)
            var batFile = new BatFile(path, new List<IAction>());

            if (!_batFiles.Contains(batFile))
                _batFiles.Add(batFile);
        }
    }

    public List<BatFile> GetBatFiles() => new List<BatFile>(_batFiles);

    public BatFile CreateBatFile(List<IAction> actions, string fileName)
    {
        if (actions == null || actions.Count == 0)
            throw new ArgumentException("��� �������� ��� �������� �������");
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("��� ����� �� �������");

        foreach (char c in Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c.ToString(), "");
        if (!fileName.EndsWith(".bat", StringComparison.OrdinalIgnoreCase))
            fileName += ".bat";

        var content = new System.Text.StringBuilder();
        foreach (var action in actions)
            content.AppendLine(action.ToBatCommand());

        // ���������� ���� _folder ������ ��������� ����������
        if (!Directory.Exists(_folder)) // *** �������� ***
            Directory.CreateDirectory(_folder); // *** �������� ***

        string filePath = Path.Combine(_folder, fileName); // *** �������� ***

        File.WriteAllText(filePath, content.ToString());
        var batFile = new BatFile(filePath, actions);
        if (!_batFiles.Contains(batFile)) // *** ��������� ***
            _batFiles.Add(batFile); // *** ��������� ***
        return batFile;
    }

    public void RunBatFile(BatFile batFile)
    {
        if (!File.Exists(batFile.FilePath))
            throw new FileNotFoundException("���� �� ������", batFile.FilePath);

        var processInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = batFile.FilePath,
            UseShellExecute = true // *** ��������� ��� ����������� ������� ***
        };
        System.Diagnostics.Process.Start(processInfo);
    }
}
