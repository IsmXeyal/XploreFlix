﻿using System.ComponentModel.DataAnnotations;

namespace XploreFlixDomainLayer.Entities;

public class Actor
{
	public int Id { get; set; }
	[Required]
	public string? Name { get; set; }
	public byte[]? Image { get; set; }
	[Required]
	public string? Bio { get; set; }

	public virtual List<MovieActor>? MovieActors { get; set; }
}
