using UnityEngine;
using System.Collections;

public class Vehicle{

	private string TypeVehicle; // Tipos: Cavalo, Navio
	private float Payload; // Carga atrelado ao veiculo


	Vehicle(string TypeVehicle, float Payload){
		this.TypeVehicle = TypeVehicle;
		this.Payload = Payload;
	}

	public string GetTypeVehicle(){
		return this.TypeVehicle;
	}

	public float GetPayload(){
		return this.Payload;
	}
}

