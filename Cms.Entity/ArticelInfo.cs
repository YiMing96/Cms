using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ArticelInfo : BaseEntity<long>
    {
        /// <summary>
        /// 关 键 字
        /// </summary>
        public string? KeyWords { get; set; }
        /// <summary>
        /// 标题类型，例如：公告，图文，推荐等类型
        /// </summary>
        public string? TitleType { get; set; }
        /// <summary>
        /// 文章简短标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        ///  完整标题
        /// </summary>
        public string? FullTitle { get; set; }
        /// <summary>
        /// 文章导读
        /// </summary>
        public string? Intro { get; set; }
        /// <summary>
        /// 文章标题颜色
        /// </summary>
        public string? TitleFontColor { get; set; }
        /// <summary>
        /// 文章标题字体类型
        /// </summary>
        public string? TitleFontType { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string? ArticelContent { get; set; }
        /// <summary>
        /// 文章的作者
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// 文章来源
        /// </summary>
        public string? Origin { get; set; }

        /// <summary>
        /// 图片的地址
        /// </summary>
        public string? PhotoUrl { get; set; }
        /// <summary>
        /// 文章属于哪个类别
        /// </summary>
        public ArticelClass? ArticelClass { get; set; }
        /// <summary>
        /// 属性作为表中的外键，属性名字一定
        /// </summary>
        public int ArticelClassId { get; set; }
        public List<ArticelComment> ArticelComments { get; set; }=new List<ArticelComment>();


        //文章点击次数
        public long Changes { get; set; }
    }
}
