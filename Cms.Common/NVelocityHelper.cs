using NVelocity.App;
using NVelocity.Runtime;
using NVelocity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Common
{
    public class NVelocityHelper
    {
        /// <summary>
        /// 创建NVelocity模板引擎，读取设计好的模板页面，替换占位符。
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <param name="templateFilePath"></param>
        /// <returns></returns>
        public static string RenderTemplate(string templateName, object data, string templateFilePath)
        {
            VelocityEngine vltEngine = new VelocityEngine(); // 创建模版引擎
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templateFilePath);
            vltEngine.Init();

            VelocityContext vltContext = new VelocityContext();
            vltContext.Put("data", data); // 把data对象中的数据传递数据给data属性，在模版中会替换对应的占位符

            Template vltTemplate = vltEngine.GetTemplate(templateName + ".html"); // 得到模版文件名称
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);

            return vltWriter.GetStringBuilder().ToString();
        }
    }
}
