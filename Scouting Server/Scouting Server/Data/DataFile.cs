using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scouting_Server.Data
{
  public class DataFile <DataModel>
  {
    string Path;
    ASCIIEncoding Enc;

    /// <summary>
    /// Creates a new datafile using the datafile
    /// </summary>
    /// <param name="path"></param>
    public DataFile(string path)
    {
      Enc = new ASCIIEncoding();
      Path = path;
      FileInfo info = new FileInfo(path);
      bool exists = info.Exists;
      var stream = info.AppendText();

      if (!exists)
      {
        CreateFile(stream);
      }

      stream.Close();
    }

    public void WriteRow(DataModel data)
    {
      FileInfo info = new FileInfo(Path);
      bool exists = info.Exists;
      var stream = info.AppendText();

      if (!exists)
      {
        CreateFile(stream);
      }

      Type t = typeof(DataModel);
      foreach (PropertyInfo m in t.GetProperties())
      {
        stream.Write(m.GetValue(data).ToString() + "\t");
      }
      stream.Write('\n');

      stream.Close();
    }

    private void CreateFile(StreamWriter stream)
    {
      Type t = typeof(DataModel);
      foreach (PropertyInfo m in t.GetProperties())
      {
        stream.Write(m.Name + '\t');
      }
      stream.Write('\n');
    }
  }
}
