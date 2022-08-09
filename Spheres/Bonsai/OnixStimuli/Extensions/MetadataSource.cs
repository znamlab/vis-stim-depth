using Bonsai;
using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

public class Metadata
{
public string Animal {get; set;}
public string RootDirectory {get; set;}
}


[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class MetadataSource
{
    public Metadata ExpInfo = new Metadata();

    public string Animal {
        get { return this.ExpInfo.Animal; }  
        set { this.ExpInfo.Animal = value; }
    }

    [Description("The name of the directory to save all data.")]
    public string RootDirectory {
        get { return this.ExpInfo.RootDirectory; }  
        set { this.ExpInfo.RootDirectory = value; }
    }

    public IObservable<Metadata> Process()
    {
        if (string.IsNullOrEmpty(ExpInfo.Animal)){
            throw new Exception("Animal must be specified!");
        }

        if (string.IsNullOrEmpty(ExpInfo.RootDirectory)){
            ExpInfo.RootDirectory = System.IO.Directory.GetCurrentDirectory();
        }
        return Observable.Return(ExpInfo);
    }
}
