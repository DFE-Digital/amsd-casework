﻿using System;
using System.Numerics;

namespace ConcernsCaseWork.Models
{
	/// <summary>
	/// Frontend model classes used only for UI rendering
	/// </summary>
	public sealed class TypeModel
	{
		/// <summary>
		/// Record, SRMA, Safeguarding, Concern
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Record (Log information when it is not a Concern)
		/// </summary>
		public string Description { get; }
		
		public DateTimeOffset CreatedAt { get; }
		
		public DateTimeOffset UpdatedAt { get; }
		
		public BigInteger Urn { get; }
		
		public TypeModel(string name, string description, DateTimeOffset createdAt, 
			DateTimeOffset updatedAt, BigInteger urn) => 
			(Name, Description, CreatedAt, UpdatedAt, Urn) = (name, description, createdAt, updatedAt, urn);
	}
}