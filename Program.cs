using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Quartz.Impl;

class Program
{
    static async Task Main(string[] args)
    {
        
        
        var hostname = Dns.GetHostName();
        Console.WriteLine("GetHostName: " + hostname);
        var hostEntry = await Dns.GetHostEntryAsync(hostname);
        Console.WriteLine("hostEntry: " + hostEntry);
        var firstIP = hostEntry.AddressList[0].ToString();
        Console.WriteLine("firstIP: " + firstIP);
        
        // this fails:
        //var firstAddressEntry = await Dns.GetHostEntryAsync(firstIP);            
        //Console.WriteLine(firstAddressEntry);

        var config = new NameValueCollection
        {
            {StdSchedulerFactory.PropertySchedulerInstanceId, StdSchedulerFactory.AutoGenerateInstanceId},
            {StdSchedulerFactory.PropertyJobStoreType, "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz"},
            {"quartz.jobStore.clustered", "true"},
            {"quartz.serializer.type", "json"},
        };
        var scheduler = await new StdSchedulerFactory(config).GetScheduler();
        
        Console.WriteLine(scheduler);
    }
    
    
}
