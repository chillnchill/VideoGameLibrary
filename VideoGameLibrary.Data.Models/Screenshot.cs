using MongoDB.Bson;
using System;

public class Screenshot
{
	public ObjectId Id { get; set; }

	public string FileName { get; set; }

	public string ContentType { get; set; }

	public long Size { get; set; }

	public ObjectId GameId { get; set; }
}
