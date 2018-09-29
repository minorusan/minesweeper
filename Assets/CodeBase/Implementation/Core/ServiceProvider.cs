using System.Collections.Generic;
using System;


namespace Core
{
	public class ServiceProvider   
	{
		private static Dictionary<Type, object> _typesDictionary;

		static ServiceProvider()
		{
			_typesDictionary = new Dictionary<Type, object>();
			_typesDictionary.Add(typeof(CommandExecutor), new CommandExecutor());
			_typesDictionary.Add(typeof(UpdateController), new UpdateController());
		}

		public static T GetService<T>() where T:class
		{
			if (_typesDictionary.ContainsKey(typeof(T)))
			{
				return _typesDictionary[typeof(T)] as T;	
			}
			return null;
		}
	}
}