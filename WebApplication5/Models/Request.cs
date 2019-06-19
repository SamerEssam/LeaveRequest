using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{

    public class Request
    {
        [Key]
        public int ID { get; set; }
        //[Required]
        public DateTime From { get; set; }
        //[Required]
        public DateTime To { get; set; }
        [ForeignKey("Requester")]
        public string RequesterID { get; set; }
        [ForeignKey("DirectManager")]
        public string MngrID { get; set; }
        public virtual ApplicationUser Requester { get; set; }
        public virtual ApplicationUser DirectManager { get; set; }
        //[Required]


        // datat anotation for enum type ???
        [ForeignKey("VacType")]
        public int VacTypeID { get; set; }
        public VacationType VacType { get; set; }
        [ForeignKey("RequestStatus")]
        public int ReqStatusID { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
}