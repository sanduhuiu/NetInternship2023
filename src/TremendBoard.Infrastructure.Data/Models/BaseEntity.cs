using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TremendBoard.Infrastructure.Data.Models
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /*public DateTime Deadline { get; set; }
        public string ProjectStatus { get; set; }*/
        // If we add these properties here we asign the BaseEntity class multiple roles:
        // 1) Keep the information about the unique identificator of each class that represents an entity
        // 2) Keep specific information about a project
        // Then, all classes that extend the base entity will have these properties and that may not be necessary
        // So, we add the properties only in Project class
    }
}
