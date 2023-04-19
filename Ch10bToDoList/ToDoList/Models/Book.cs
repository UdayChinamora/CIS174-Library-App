﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(13, ErrorMessage = "Max 13 digits")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter due date")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please enter Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please select a genre.")]
        public string GenreId { get; set; }
         public Genre Genre { get; set; }
        
        [Required(ErrorMessage = "Please enter Author")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select a status.")]
        public string StatusId { get; set; }
        public Status Status { get; set; }

        public bool Overdue => 
            StatusId == "available" && DueDate < DateTime.Today;
    }
}