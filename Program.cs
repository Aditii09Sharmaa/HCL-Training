using System;


public delegate double BillingStrategy(double amount);


public delegate void HospitalNotification(string message);


abstract class Patient
{
    public int PatientId { get; set; }
    public string Name { get; set; }

    public Patient(int id, string name)
    {
        PatientId = id;
        Name = name;
    }

    public abstract double CalculateBill();
}


class InPatient : Patient
{
    public int DaysAdmitted { get; set; }

    public InPatient(int id, string name, int days)
        : base(id, name)
    {
        DaysAdmitted = days;
    }

    public override double CalculateBill()
    {
        return DaysAdmitted * 2000;
    }
}

class OutPatient : Patient
{
    public OutPatient(int id, string name)
        : base(id, name)
    {
    }

    public override double CalculateBill()
    {
        return 500;
    }
}

class EmergencyPatient : Patient
{
    public EmergencyPatient(int id, string name)
        : base(id, name)
    {
    }

    public override double CalculateBill()
    {
        return 5000;
    }
}


class BillingService
{
    public BillingStrategy billingStrategy;

    public double GenerateBill(double amount)
    {
        return billingStrategy(amount);
    }
}


class Hospital
{
    public event HospitalNotification NotifyDepartments;

    public void TriggerNotification(string msg)
    {
        NotifyDepartments?.Invoke(msg);
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("=== Hospital Patient Management System ===\n");

        Console.Write("Enter Patient ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Patient Name: ");
        string name = Console.ReadLine();

        Console.WriteLine("\nSelect Patient Type:");
        Console.WriteLine("1. In-Patient");
        Console.WriteLine("2. Out-Patient");
        Console.WriteLine("3. Emergency");
        Console.Write("Choice: ");
        int choice = int.Parse(Console.ReadLine());

        Patient patient = null;

        switch (choice)
        {
            case 1:
                Console.Write("Enter number of days admitted: ");
                int days = int.Parse(Console.ReadLine());
                patient = new InPatient(id, name, days);
                break;

            case 2:
                patient = new OutPatient(id, name);
                break;

            case 3:
                patient = new EmergencyPatient(id, name);
                break;

            default:
                Console.WriteLine("Invalid choice!");
                return;
        }

        
        double baseBill = patient.CalculateBill();

        
        BillingService billingService = new BillingService();

       
        billingService.billingStrategy = (amount) => amount + (amount * 0.10);

        double finalBill = billingService.GenerateBill(baseBill);

        
        Hospital hospital = new Hospital();
        hospital.NotifyDepartments += NotifyAccounts;
        hospital.NotifyDepartments += NotifyPharmacy;

        Console.WriteLine("\n----- BILL GENERATED -----");
        Console.WriteLine($"Patient Name: {patient.Name}");
        Console.WriteLine($"Base Amount: ₹{baseBill}");
        Console.WriteLine($"Final Amount (After Tax): ₹{finalBill}");

        hospital.TriggerNotification("New patient bill generated.");

        Console.WriteLine("\nProcess Completed Successfully.");
    }

    
    static void NotifyAccounts(string message)
    {
        Console.WriteLine("Accounts Dept Notification: " + message);
    }

    static void NotifyPharmacy(string message)
    {
        Console.WriteLine("Pharmacy Dept Notification: " + message);
    }
}
