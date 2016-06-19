using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWeb
{
	public abstract class BaseAbstractAppStart
	{
		public virtual string Methoda()
		{
			return "a";
		}

		public abstract string AbstractMethod();
	}

	public class DerivedAbstractAppStart : BaseAbstractAppStart
	{
		public override string Methoda()
		{
			var baseMethod = base.Methoda();
			return baseMethod + " Hello...";
		}

		public override string AbstractMethod()
		{
			return "Hello";
		}
	}

	public class BaseAppStart
	{
		public virtual string Methoda()
		{
			return "a";
		}
	}

	public class DerivedAppStart : BaseAppStart
	{
		public override string Methoda()
		{
			var baseMethod = base.Methoda();
			return baseMethod + " Hello...";
		}

	}

}
