using System;
using System.Threading.Tasks;

namespace Scouter.Models
{
    [Obsolete("Do not use", true)]
    public class zzSize
    {
        public int Id { get; set; }
        /// <summary>
        /// inches of Length
        /// </summary>
        public float Length { get; set; }
        /// <summary>
        /// inches of Width
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// inches of Height
        /// </summary>
        public float Height { get; set; }
    }
}
