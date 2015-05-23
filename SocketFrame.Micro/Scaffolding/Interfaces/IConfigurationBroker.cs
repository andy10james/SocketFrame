using System;

public interface IConfigurationBroker {
	
	public void SetBacklog(Int16 backlog);
	public void SetSessionTimeout(Int64 timeout);
	public void SetMaxCommandLength(Int64 maxLength);
	
	public Int16 GetBacklog();
	public Int64 GetSessionTimeout();
	public Int64 GetMaxCommandLength();
	
}