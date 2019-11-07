﻿using Maverick.Domain.Adapters;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Microsoft.Extensions.Logging;
//using Otc.Validations.Helpers;
using System;
using Otc.Validations.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maverick.Application
{
    public class FilmesService : IFilmesService
    {
        private readonly ITmdbAdapter tmdbAdapter;
        private readonly ApplicationConfiguration configuration;
        private readonly ILogger logger;

        public FilmesService(ITmdbAdapter tmdbAdapter, ApplicationConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.tmdbAdapter = tmdbAdapter ?? throw new ArgumentNullException(nameof(tmdbAdapter));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            logger = loggerFactory?.CreateLogger<FilmesService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<Filme>> ObterFilmesAsync(Pesquisa pesquisa)
        {
            if (pesquisa == null)
            {
                throw new ArgumentNullException(nameof(pesquisa));
            }


            try
            {
                ValidationHelper.ThrowValidationExceptionIfNotValid(pesquisa);
            }
            catch(Exception ex)
            {
                throw;
            }

            ValidationHelper.ThrowValidationExceptionIfNotValid(pesquisa);
            logger.LogInformation("Realizando chamada ao TMDb com os seguintes " +
                "criterios de pesquisa: {@CriteriosPesquisa}", new { Criterios = pesquisa, configuration.Idioma });
            IEnumerable<Filme> resultado = await tmdbAdapter.GetFilmesAsync(pesquisa, configuration.Idioma);
            logger.LogInformation("Chamada ao TMDb concluida com sucesso.");

            return resultado;
        }
    }
}
