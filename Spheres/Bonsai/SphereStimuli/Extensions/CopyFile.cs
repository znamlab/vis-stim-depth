using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.IO;

[Combinator]
[Description("Copies a target file.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CopyFile
{
    private string sourcePath;
    [Editor("Bonsai.Design.OpenFileNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
    [Description("Name of the source file to be copied.")]
    public string SourcePath
    {
        get { return sourcePath; }
        set { sourcePath = value; }
    }

    private string destinationPath;
    [Editor("Bonsai.Design.SaveFileNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
    [Description("Name of the destination file.")]
    public string DestinationPath
    {
        get { return destinationPath; }
        set { destinationPath = value; }
    }

    private bool overwrite;
    public bool Overwrite
    {
        get { return overwrite; }
        set { overwrite = value; }
    }

    public IObservable<bool> Process(IObservable<Object> source)
    {
        return source.Select(value => {
            File.Copy(sourcePath, destinationPath, overwrite);
            return true;
        });
    }
}
