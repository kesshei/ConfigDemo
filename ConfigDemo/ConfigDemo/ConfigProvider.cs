using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigDemo
{
    /// <summary>
    /// 配置提供者
    /// </summary>
    public class ConfigProvider : ConfigurationProvider, IDisposable
    {
        private bool isDisposed = false;
        public ConfigProvider()
        {
            ThreadPool.QueueUserWorkItem(obj =>
            {
                while (!isDisposed)
                {
                    Load();
                    Thread.Sleep(3 * 1000);
                }
            });
        }
        /// <summary>
        /// 读写锁
        /// </summary>
        private ReaderWriterLockSlim lockObj = new ReaderWriterLockSlim();
        public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            lockObj.EnterReadLock();
            try
            {
                return base.GetChildKeys(earlierKeys, parentPath);
            }
            finally
            {
                lockObj.ExitReadLock();
            }
        }
        public override bool TryGet(string key, out string value)
        {
            lockObj.EnterReadLock();
            try
            {
                return base.TryGet(key, out value);
            }
            finally
            {
                lockObj.ExitReadLock();
            }
        }
        /// <summary>
        /// 更新数据
        /// 内部数据结构: Name
        ///              Name:asdfd:asdf:0
        ///              Name:asdfd:asdf:1
        /// </summary>
        public override void Load()
        {
            try
            {
                lockObj.EnterWriteLock();
                //模拟动态更改指定配置，后期可以用配置中心 数据库 信息读取等实现
                Data["AliOSSConfig:BucketName"] = DateTime.Now.ToString();
            }
            finally
            {
                lockObj.ExitWriteLock();
            }
            OnReload();
        }
        public void Dispose()
        {
            isDisposed = true;
        }
    }
}
