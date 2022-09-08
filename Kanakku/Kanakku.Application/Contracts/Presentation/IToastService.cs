using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanakku.Application.Contracts.Presentation;

public interface IToastService
{

    void Success(string content, int duration = 3);
    
    void Error(string content, int duration = 3);
}
