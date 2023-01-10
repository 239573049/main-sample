using System.Collections.Concurrent;
using Token.Dependency;

namespace Video.Storage;

public class StorageManage : ISingletonDependency
{
    /// <summary>
    /// 共享数据
    /// </summary>
    public Dictionary<object, object> Items { get; set; } = new ();

}
