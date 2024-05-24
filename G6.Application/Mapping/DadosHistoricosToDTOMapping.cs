using AutoMapper;
using G6.Application.DTOs;
using G6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.Mapping
{
    public class DadosHistoricosToDTOMapping : Profile
    {
       
            public DadosHistoricosToDTOMapping()
            {
                CreateMap<DadosHistoricosAtivosDTO, DadosHistoricosAtivos>();
                CreateMap<DadosHistoricosAtivos, DadosHistoricosAtivosDTO>();
            }
        
    }
}
