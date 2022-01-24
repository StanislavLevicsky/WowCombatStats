using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WowCombatStats.Models.CustomAttributes;

namespace WowCombatStats.Models.ViewModels
{
    public class UploadVM
    {
        [Required]
        public int ServerId { get; set; }

        [Required]
        [AllowedExtensionsAttribute(new string[] { ".zip" })]
        public IFormFile File { get; set; }

		public SelectList? Servers { get; set; }
	}
}
