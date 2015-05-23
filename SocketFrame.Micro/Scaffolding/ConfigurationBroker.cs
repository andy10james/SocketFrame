using System;

public class ConfigurationBroker {

	private static readonly ConfigurationBroker _instance = new ConfigurationBroker();
	
	private Int16 _backlog;
	private Int64 _timeout;
	private Int64 _maxLength;
	
	public static ConfigurationBroker Instance {
		get { return _instance; }
	}
	
	public void SetBacklog(Int16 backlog) {
		this._backlog = backlog;
	}
	
	public void SetSessionTimeout(Int64 timeout) {
		this._timeout = timeout;
	}
	
	public void SetMaxCommandLength(Int64 maxLength) {
		this._maxLength = maxLength;
	}
	
	public Int16 GetBacklog() {
		return this._backlog;
	}
	
	public Int64 GetSessionTimeout() {
		return this._timeout;
	}
	
	public Int64 GetMaxCommandLength() {
		return this._maxLength;
	}
	
}