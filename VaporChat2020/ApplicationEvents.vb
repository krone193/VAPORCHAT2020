Namespace My
	' Per MyApplication sono disponibili gli eventi seguenti:
	' Startup: generato all'avvio dell'applicazione, prima della creazione del form di avvio.
	' Shutdown: generato dopo la chiusura di tutti i form dell'applicazione. Questo evento non viene generato se l'applicazione termina in modo anomalo.
	' UnhandledException: generato se nell'applicazione si verifica un'eccezione non gestita.
	' StartupNextInstance: generato all'avvio di un'applicazione a istanza singola se l'applicazione è già attiva. 
	' NetworkAvailabilityChanged: generato quando la connessione di rete viene connessa o disconnessa.
	Partial Friend Class MyApplication
		Private Sub AppStart(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
			AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssemblies
		End Sub

		Private Function ResolveAssemblies(sender As Object, e As ResolveEventArgs) As Reflection.Assembly
			Dim desiredAssembly = New Reflection.AssemblyName(e.Name)
			If desiredAssembly.Name = "MQTTnet" Then
				Return Reflection.Assembly.Load(My.Resources.MQTTnet)
			Else
				Return Nothing
			End If
		End Function

	End Class
End Namespace
