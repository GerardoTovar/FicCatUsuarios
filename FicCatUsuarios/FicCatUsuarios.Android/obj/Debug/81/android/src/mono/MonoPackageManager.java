package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);
				
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();
				java.io.File external0 = android.os.Environment.getExternalStorageDirectory ();
				String externalDir = new java.io.File (
							external0,
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();
				String externalLegacyDir = new java.io.File (
							external0,
							"../legacy/Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new String[] {
							externalDir,
							externalLegacyDir
						},
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "FicCatUsuarios.Android.dll" comes first in this list. */
		"FicCatUsuarios.Android.dll",
		"Autofac.dll",
		"Autofac.Extensions.DependencyInjection.dll",
		"AutoFac.Extras.NLog.DotNetCore.dll",
		"FicCatUsuarios.dll",
		"FormsViewGroup.dll",
		"Microsoft.AspNetCore.Hosting.Abstractions.dll",
		"Microsoft.AspNetCore.Hosting.Server.Abstractions.dll",
		"Microsoft.AspNetCore.Http.Abstractions.dll",
		"Microsoft.AspNetCore.Http.dll",
		"Microsoft.AspNetCore.Http.Extensions.dll",
		"Microsoft.AspNetCore.Http.Features.dll",
		"Microsoft.AspNetCore.Routing.Abstractions.dll",
		"Microsoft.AspNetCore.WebUtilities.dll",
		"Microsoft.Data.Sqlite.dll",
		"Microsoft.DotNet.PlatformAbstractions.dll",
		"Microsoft.EntityFrameworkCore.Abstractions.dll",
		"Microsoft.EntityFrameworkCore.dll",
		"Microsoft.EntityFrameworkCore.Relational.dll",
		"Microsoft.EntityFrameworkCore.Sqlite.dll",
		"Microsoft.EntityFrameworkCore.SqlServer.dll",
		"Microsoft.Extensions.Caching.Abstractions.dll",
		"Microsoft.Extensions.Caching.Memory.dll",
		"Microsoft.Extensions.Configuration.Abstractions.dll",
		"Microsoft.Extensions.Configuration.Binder.dll",
		"Microsoft.Extensions.Configuration.dll",
		"Microsoft.Extensions.DependencyInjection.Abstractions.dll",
		"Microsoft.Extensions.DependencyInjection.dll",
		"Microsoft.Extensions.DependencyModel.dll",
		"Microsoft.Extensions.FileProviders.Abstractions.dll",
		"Microsoft.Extensions.Hosting.Abstractions.dll",
		"Microsoft.Extensions.Logging.Abstractions.dll",
		"Microsoft.Extensions.Logging.dll",
		"Microsoft.Extensions.ObjectPool.dll",
		"Microsoft.Extensions.Options.dll",
		"Microsoft.Extensions.Primitives.dll",
		"Microsoft.Net.Http.Headers.dll",
		"Newtonsoft.Json.dll",
		"NLog.dll",
		"NLog.Extensions.Logging.dll",
		"NLog.Web.AspNetCore.dll",
		"Remotion.Linq.dll",
		"SQLitePCLRaw.batteries_green.dll",
		"SQLitePCLRaw.batteries_v2.dll",
		"SQLitePCLRaw.core.dll",
		"SQLitePCLRaw.lib.e_sqlite3.dll",
		"SQLitePCLRaw.provider.e_sqlite3.dll",
		"Syncfusion.Core.XForms.Android.dll",
		"Syncfusion.Core.XForms.dll",
		"Syncfusion.Data.Portable.dll",
		"Syncfusion.GridCommon.Portable.dll",
		"Syncfusion.Licensing.dll",
		"Syncfusion.SfAutoComplete.Android.dll",
		"Syncfusion.SfAutoComplete.XForms.Android.dll",
		"Syncfusion.SfAutoComplete.XForms.dll",
		"Syncfusion.SfDataGrid.XForms.Android.dll",
		"Syncfusion.SfDataGrid.XForms.dll",
		"Syncfusion.SfNumericTextBox.Android.dll",
		"Syncfusion.SfNumericTextBox.XForms.Android.dll",
		"Syncfusion.SfNumericTextBox.XForms.dll",
		"Syncfusion.SfNumericUpDown.Android.dll",
		"Syncfusion.SfNumericUpDown.XForms.Android.dll",
		"Syncfusion.SfNumericUpDown.XForms.dll",
		"System.Buffers.dll",
		"System.Collections.Immutable.dll",
		"System.Data.SQLite.dll",
		"System.Diagnostics.DiagnosticSource.dll",
		"System.Interactive.Async.dll",
		"System.Memory.dll",
		"System.Runtime.CompilerServices.Unsafe.dll",
		"System.Text.Encodings.Web.dll",
		"Xamarin.Android.Arch.Core.Common.dll",
		"Xamarin.Android.Arch.Lifecycle.Common.dll",
		"Xamarin.Android.Arch.Lifecycle.Runtime.dll",
		"Xamarin.Android.Support.Animated.Vector.Drawable.dll",
		"Xamarin.Android.Support.Annotations.dll",
		"Xamarin.Android.Support.Compat.dll",
		"Xamarin.Android.Support.Core.UI.dll",
		"Xamarin.Android.Support.Core.Utils.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.Fragment.dll",
		"Xamarin.Android.Support.Media.Compat.dll",
		"Xamarin.Android.Support.Transition.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.MediaRouter.dll",
		"Xamarin.Android.Support.v7.Palette.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Android.Support.Vector.Drawable.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Platform.dll",
		"Xamarin.Forms.Xaml.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_27";
}
