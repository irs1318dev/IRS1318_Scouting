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
    char Delimiter;
    Dictionary<ulong, DataModel> rows;
    Dictionary<string, PropertyInfo> properties;
    ulong id;

    /// <summary>
    /// Creates a new datafile using the datafile
    /// </summary>
    /// <param name="path">the path for the file</param>
    /// <param name="delimiter">the delimiter to separate columns </param>
    public DataFile(string path, char delimiter = ',')
    {
      rows = new Dictionary<ulong, DataModel>();
      properties = new Dictionary<string, PropertyInfo>();
      Delimiter = delimiter;
      Enc = new ASCIIEncoding();
      Path = path;
      FileInfo info = new FileInfo(path);
      bool exists = info.Exists;

      Type t = typeof(DataModel);

      var props = t.GetProperties();

      foreach(var prop in props)
      {
        properties.Add(prop.Name.ToLower(), prop);
      }

      try
      {
        var temp = properties["id"];

      }
      catch
      {
        throw new Exception("DataModel must contain an id field of type ulong!");
      }

      if (properties["id"].PropertyType != typeof(ulong))
        throw new Exception("DataModel's id field must be of type ulong!");

      if (exists)
      {
        Load();
      }
    }

    public ulong Add(DataModel data)
    {
      properties["id"].SetValue(data, id, null);
      rows.Add(id, data);
      return id++;
    }

    public void Update(DataModel data, ulong id)
    {
      if (rows.ContainsKey(id))
      {
        properties["id"].SetValue(data, id, null);
        rows[id] = data;
      }
      else
        throw new InvalidOperationException("Data does not contain a row with ID: " + id);
    }

    public void Update(DataModel data)
    {
      ulong id = (ulong)properties["id"].GetValue(data, null);

      Update(data, id);
    }

    public void Remove(ulong id)
    {
      if (rows.ContainsKey(id))
        rows.Remove(id);
      else
        throw new InvalidOperationException("Data does not contain a row with ID: " + id);
    }

    public void Remove(DataModel data)
    {
      ulong id = (ulong)properties["id"].GetValue(data, null);

      Remove(id);
    }

    public bool Exists(ulong id)
    {
      return rows.ContainsKey(id);
    }

    public DataModel Get(ulong id)
    {
      return rows[id];
    }

    public DataModel[] GetAll()
    {
      return rows.Values.ToArray();
    }

    public void Save()
    {
      FileInfo info = new FileInfo(Path);
            try
            {
                StreamWriter stream = new StreamWriter(info.Open(FileMode.Create));


                WriteHeaders(stream);
                foreach (var pair in rows)
                {
                    WriteRow(pair.Value, stream);
                }

                stream.Close();
            }
            catch
            {
                return;
            }

    }

    private void Load()
    {
      FileInfo info = new FileInfo(Path);
      StreamReader stream = new StreamReader(info.Open(FileMode.Open));

      //get rid of the first line
      stream.ReadLine();

      ConstructorInfo ctor = typeof(DataModel).GetConstructor(Type.EmptyTypes);

      while (!stream.EndOfStream)
      {
        string line = stream.ReadLine();
        string[] data = line.Split(Delimiter);
        DataModel mod = (DataModel)ctor.Invoke(null);

        for (int i = 0; i < properties.Values.Count; ++i)
        {
          PropertyInfo prop = properties.Values.ElementAt(i);

          prop.SetValue(mod, Convert.ChangeType(data[i], prop.PropertyType), null);
        }
        ulong id = (ulong)properties["id"].GetValue(mod, null);

        if (this.id <= id)
          this.id = id + 1;

        rows.Add(id, mod);
      }

      stream.Close();
    }

    private void WriteRow(DataModel data, StreamWriter stream)
    {
      foreach (PropertyInfo m in properties.Values)
      {
        stream.Write(m.GetValue(data, null).ToString() + Delimiter);
      }

      stream.Write('\n');
    }

    private void WriteHeaders(StreamWriter stream)
    {
      foreach (PropertyInfo m in properties.Values)
      {
        stream.Write(m.Name + Delimiter);
      }
      stream.Write('\n');
    }
  }
}
