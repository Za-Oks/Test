using System.Collections;

namespace VoxelBusters.NativePlugins.Internal
{
	public interface IRateMyAppOperationHandler
	{
		void Execute(IEnumerator _routine);
	}
}
