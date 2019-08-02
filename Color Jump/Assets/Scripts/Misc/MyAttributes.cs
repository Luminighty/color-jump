using System;

[AttributeUsage(AttributeTargets.Class)]
public class CustomClassName : System.Attribute 
{
	private string name;

	public CustomClassName(string name) {
		this.name = name;
	}

	public static string GetCustomName(Type t) {
		foreach (System.Attribute a in t.GetCustomAttributes(false)){
			if(a is CustomClassName)
				return ((CustomClassName)a).GetName();
		}
		return t.Name;
	}
	public static string GetCustomName(object o) {
		return GetCustomName(o.GetType());
	}


	public string GetName() {return name;}
}