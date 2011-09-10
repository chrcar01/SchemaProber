using Commons.Collections;
using NVelocity;
using NVelocity.App;
using System;
using System.Collections.Generic;
using System.IO;

namespace NVelocityProber
{
	public class VelociWrapper
	{
		private string _templateFileName;
		private string _templateDirectory;
		public VelociWrapper(string templateDirectory, string templateFileName)
		{
			_templateFileName = templateFileName;
			_templateDirectory = templateDirectory;
		}
		private Dictionary<string, object> _props;
		public VelociWrapper AddProperty(string name, object value)
		{
			if (_props == null) _props = new Dictionary<string, object>();
			_props.Add(name, value);
			return this;
		}
		private VelocityEngine CreateVelocityEngine()
		{
			var velocity = new VelocityEngine();
			var props = new ExtendedProperties();
			props.SetProperty("file.resource.loader.path", _templateDirectory);
			velocity.Init(props);
			return velocity;
		}

		public string Render()
		{
			var velocity = CreateVelocityEngine();
			var template = velocity.GetTemplate(_templateFileName);
			var context = new VelocityContext();
			if (_props != null)
			{
				foreach (var kv in _props)
				{
					context.Put(kv.Key, kv.Value);
				}
			}
			var result = String.Empty;
			using (var textWriter = new StringWriter())
			{
				template.Merge(context, textWriter);
				result = textWriter.ToString();
			}
			return result;
		}
	}
}
