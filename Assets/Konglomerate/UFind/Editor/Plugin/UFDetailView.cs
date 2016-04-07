namespace UFind
{
	public interface IDetailView
	{
		void Draw(IFinderContext context, IFinderResult result);
	}

	public abstract class UFDetailView : IDetailView
	{
		public abstract void Draw(IFinderContext context, IFinderResult result);
	}
}