using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void Callback();
public delegate void Callback<T1>(T1 arg1);
public delegate void Callback<T1, T2>(T1 arg1, T2 arg2);
public delegate void Callback<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
public delegate void Callback<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
public delegate void Callback<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
public delegate void Callback<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
public delegate void Callback<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
public delegate void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
public delegate void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

public delegate void Callback1<T1>(ref T1 arg1);

public delegate void Callback11<T1, T2>(T1 arg1, ref T2 arg2);
public delegate void Callback2<T1, T2>(ref T1 arg1, ref T2 arg2);

public delegate void Callback21<T1, T2, T3>(T1 arg1, T2 arg2, ref T3 arg3);
public delegate void Callback12<T1, T2, T3>(T1 arg1, ref T2 arg2, ref T3 arg3);
public delegate void Callback3<T1, T2, T3>(ref T1 arg1, ref T2 arg2, ref T3 arg3);

public delegate void Callback31<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4);
public delegate void Callback22<T1, T2, T3, T4>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4);
public delegate void Callback13<T1, T2, T3, T4>(T1 arg1, ref  T2 arg2, ref T3 arg3, ref T4 arg4);
public delegate void Callback4<T1, T2, T3, T4>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4);

public delegate void Callback41<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5);
public delegate void Callback32<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5);
public delegate void Callback23<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5);
public delegate void Callback14<T1, T2, T3, T4, T5>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5);
public delegate void Callback5<T1, T2, T3, T4, T5>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5);

public delegate void Callback51<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6);
public delegate void Callback42<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5, ref T6 arg6);
public delegate void Callback33<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6);
public delegate void Callback24<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6);
public delegate void Callback15<T1, T2, T3, T4, T5, T6>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6);
public delegate void Callback6<T1, T2, T3, T4, T5, T6>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6);

public delegate void Callback61<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ref T7 arg7);
public delegate void Callback52<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6, ref T7 arg7);
public delegate void Callback43<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7);
public delegate void Callback34<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7);
public delegate void Callback25<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7);
public delegate void Callback16<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7);
public delegate void Callback7<T1, T2, T3, T4, T5, T6, T7>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7);

public delegate void Callback71<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, ref T8 arg8);
public delegate void Callback62<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ref T7 arg7, ref T8 arg8);
public delegate void Callback53<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8);
public delegate void Callback44<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8);
public delegate void Callback35<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8);
public delegate void Callback26<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8);
public delegate void Callback17<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8);
public delegate void Callback8<T1, T2, T3, T4, T5, T6, T7, T8>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8);

public delegate void Callback81<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, ref T9 arg9);
public delegate void Callback72<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback63<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback54<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback45<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback36<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback27<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback18<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);
public delegate void Callback9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6, ref T7 arg7, ref T8 arg8, ref T9 arg9);

static public class Messenger {
	
	static public Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();
	//即使在Cleanup的时候处于消息处理列表permanentMessages里的页不应该被移除
	static public List< string > permanentMessages = new List< string > ();
	//标记某一信息为永久的
	static public void MarkAsPermanent(string eventType) {
		permanentMessages.Add( eventType );
	}
	
	//清空消息队列
	static public void Cleanup()
	{
		List< string > messagesToRemove = new List<string>();
		
		foreach (KeyValuePair<string, Delegate> pair in eventTable) {
			bool wasFound = false;
			
			foreach (string message in permanentMessages) {
				if (pair.Key == message) {
					wasFound = true;  
					break;
				}
			}
			
			if (!wasFound)
				messagesToRemove.Add( pair.Key );
		}
		
		foreach (string message in messagesToRemove) {
			eventTable.Remove( message );
		}
	}
	
	static public void PrintEventTable()
	{
		Debug.Log("\t\t\t=== 打印所有事件 ===");
		
		foreach (KeyValuePair<string, Delegate> pair in eventTable) {
			Debug.Log("\t\t\t" + pair.Key + "\t\t" + pair.Value);
		}
		
		Debug.Log("\n");
	}

	//增加侦听器
	static public void OnListenerAdding(string eventType, Delegate listenerBeingAdded) {
		if (!eventTable.ContainsKey(eventType)) {
			eventTable.Add(eventType, null );
		}
		
		Delegate d = eventTable[eventType];
		if (d != null && d.GetType() != listenerBeingAdded.GetType()) {
			throw new ListenerException(string.Format("{0} 事件的已注册类型 {1} 与当前添加的类型 {2} 不匹配！", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
		}
	}

	//移除侦听器
	static public void OnListenerRemoving(string eventType, Delegate listenerBeingRemoved) {
		if (eventTable.ContainsKey(eventType)) {
			Delegate d = eventTable[eventType];
			
			if (d == null) {
				throw new ListenerException(string.Format("被移除的 {0} 事件类型的侦听者为空！", eventType));
			} else if (d.GetType() != listenerBeingRemoved.GetType()) {
				throw new ListenerException(string.Format("{0}事件的已注册类型 {1} 与当前移除的类型 {2}不匹配！", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
			}
		} else {
			throw new ListenerException(string.Format("不存在 {0} 事件类型！", eventType));
		}
	}
	
	static public void OnListenerRemoved(string eventType) {
		if (eventTable[eventType] == null) {
			eventTable.Remove(eventType);
		}
	}
	
	static public BroadcastException CreateBroadcastSignatureException(string eventType) {
		return new BroadcastException(string.Format(" {0} 事件类型的署名不匹配！", eventType));
	}
	
	public class BroadcastException : Exception {
		public BroadcastException(string msg)
		: base(msg) {
		}
	}
	
	public class ListenerException : Exception {
		public ListenerException(string msg)
		: base(msg) {
		}
	}


	static public bool HasListener(string eventType, Callback handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1>(string eventType, Callback<T1> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2>(string eventType, Callback<T1, T2> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}

		return has;
	}

	static public bool HasListener<T1, T2, T3>(string eventType, Callback<T1, T2, T3> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2, T3, T4>(string eventType, Callback<T1, T2, T3, T4> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2, T3, T4, T5>(string eventType, Callback<T1, T2, T3, T4, T5> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2, T3, T4, T5, T6>(string eventType, Callback<T1, T2, T3, T4, T5, T6> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2, T3, T4, T5, T6, T7>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2, T3, T4, T5, T6, T7, T8>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7, T8> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	static public bool HasListener<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler){
		bool has = false;
		if(eventTable.Count > 0 && eventTable.ContainsKey(eventType)){
			Delegate[] list = eventTable[eventType].GetInvocationList();
			if(Array.IndexOf(list,handler) != -1){
				has = true;
			}
		}
		return has;
	}

	//增加侦听器部分
	static public void AddListener(string eventType, Callback handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback)eventTable[eventType] + handler;
	}

	static public void AddListener<T1>(string eventType, Callback<T1> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2>(string eventType, Callback<T1, T2> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3>(string eventType, Callback<T1, T2, T3> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3, T4>(string eventType, Callback<T1, T2, T3, T4> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3, T4, T5>(string eventType, Callback<T1, T2, T3, T4, T5> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3, T4, T5, T6>(string eventType, Callback<T1, T2, T3, T4, T5, T6> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3, T4, T5, T6, T7>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6, T7>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3, T4, T5, T6, T7, T8>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7, T8> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6, T7, T8>)eventTable[eventType] + handler;
	}

	static public void AddListener<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler) {
		if(HasListener(eventType,handler)){
			return;
		}
		OnListenerAdding(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>)eventTable[eventType] + handler;
	}

	//移除侦听器部分
	static public void RemoveListener(string eventType, Callback handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);   
		eventTable[eventType] = (Callback)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1>(string eventType, Callback<T1> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2>(string eventType, Callback<T1, T2> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3>(string eventType, Callback<T1, T2, T3> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3, T4>(string eventType, Callback<T1, T2, T3, T4> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3, T4, T5>(string eventType, Callback<T1, T2, T3, T4, T5> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3, T4, T5, T6>(string eventType, Callback<T1, T2, T3, T4, T5, T6> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3, T4, T5, T6, T7>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6, T7>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3, T4, T5, T6, T7, T8>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7, T8> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6, T7, T8>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}

	static public void RemoveListener<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventType, Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler) {
		if(!HasListener(eventType,handler)){
			return;
		}
		OnListenerRemoving(eventType, handler);
		eventTable[eventType] = (Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>)eventTable[eventType] - handler;
		OnListenerRemoved(eventType);
	}
	
	//发送部分
	static public void Broadcast(string eventType) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback callback = d as Callback;
			
			if (callback != null) {
				callback();
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1>(string eventType, T1 arg1) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1> callback = d as Callback<T1>;
			
			if (callback != null) {
				callback(arg1);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2>(string eventType, T1 arg1, T2 arg2) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2> callback = d as Callback<T1, T2>;
			
			if (callback != null) {
				callback(arg1, arg2);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3> callback = d as Callback<T1, T2, T3>;
			
			if (callback != null) {
				callback(arg1, arg2, arg3);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3, T4> callback = d as Callback<T1, T2, T3, T4>;

			if (callback != null) {
				callback(arg1, arg2, arg3, arg4);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3, T4, T5>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3, T4, T5> callback = d as Callback<T1, T2, T3, T4, T5>;

			if (callback != null) {
				callback(arg1, arg2, arg3, arg4, arg5);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3, T4, T5, T6>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3, T4, T5, T6> callback = d as Callback<T1, T2, T3, T4, T5, T6>;

			if (callback != null) {
				callback(arg1, arg2, arg3, arg4, arg5, arg6);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3, T4, T5, T6, T7>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3, T4, T5, T6, T7> callback = d as Callback<T1, T2, T3, T4, T5, T6, T7>;

			if (callback != null) {
				callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3, T4, T5, T6, T7, T8>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3, T4, T5, T6, T7, T8> callback = d as Callback<T1, T2, T3, T4, T5, T6, T7, T8>;

			if (callback != null) {
				callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}

	static public void Broadcast<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) {
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d)) {
			Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback = d as Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>;

			if (callback != null) {
				callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
			} else {
				//throw CreateBroadcastSignatureException(eventType);
			}
		}
	}
}
