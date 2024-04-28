﻿using ShortLinkGeneration.Infrastructure;

namespace ShortLinkGeneration;

/// <summary>
/// 显示启动图
/// </summary>
[AddHostedService]
public class StartupArt : IHostedService
{
    /// <summary>
    /// 显示启动图
    /// </summary>
    private void Print()
    {
        Console.WriteLine(@"
 ══════════════════════════════════════════════════════════════════════ 
                                                                        
       ███████╗  ██████╗   ██████╗  ██████╗ ██████╗  ██╗   ██╗ ██╗      
       ╚══███╔╝ ██╔════╝  ██╔════╝ ██╔════╝ ██╔══██╗ ██║   ██║ ██║      
         ███╔╝  ██║  ███╗ ██║      ██║      ██████╔╝ ██║   ██║ ██║      
        ███╔╝   ██║   ██║ ██║      ██║      ██╔══██╗ ██║   ██║ ██║      
       ███████╗ ╚██████╔╝ ╚██████╗ ╚██████╗ ██║  ██║ ╚██████╔╝ ██║      
       ╚══════╝  ╚═════╝   ╚═════╝  ╚═════╝ ╚═╝  ╚═╝  ╚═════╝  ╚═╝      
                                                                        
                            Powered By ZGCCRUI                     
                        https://github.com/cr-zhichen               
                                                                        
 ══════════════════════════════════════════════════════════════════════  
");
    }

    /// <summary>
    /// 显示启动图
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Print();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}