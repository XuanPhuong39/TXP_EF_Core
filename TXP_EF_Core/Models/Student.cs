﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TXP_EF_Core.Models;

[Index("GradeId", Name = "IX_Students_GradeId")]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public int GradeId { get; set; }

    [ForeignKey("GradeId")]
    [InverseProperty("Students")]
    public virtual Grade Grade { get; set; }
}