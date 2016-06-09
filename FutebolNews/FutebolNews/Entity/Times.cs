using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FutebolNews.Entity
{
    public class Times
    {
        public string nome;
        public string link;

        public string getNome { get { return nome; } }

        public string getLink { get { return link; } }
    }

    public class CatalogoTimes
    {
        static List<Times> meusTimes = new List<Times>
        {
            new Times { nome  = "Atlético MG",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/atletico-mg/feed.rss" },
            new Times { nome  = "Atlético PR",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/atletico-pr/feed.rss" },
            new Times { nome  = "Avaí",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/avai/feed.rss" },
            new Times { nome  = "Bahia",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/bahia/feed.rss" },
            new Times { nome  = "Botafogo",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/botafogo/feed.rss" },
            new Times { nome  = "Corinthians",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/corinthians/feed.rss" },
            new Times { nome  = "Coritiba",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/coritiba/feed.rss" },
            new Times { nome  = "Cruzeiro",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/cruzeiro/feed.rss" },
            new Times { nome  = "Figueirense",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/figueirense/feed.rss" },
            new Times { nome  = "Flamengo",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/flamengo/feed.rss" },
            new Times { nome  = "Fluminense",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/fluminense/feed.rss" },
            new Times { nome  = "Goiás",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/goias/feed.rss" },
            new Times { nome  = "Grêmio",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/gremio/feed.rss" },
            new Times { nome  = "Internacional",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/internacional/feed.rss" },
            new Times { nome  = "Juventude",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/juventude/feed.rss" },
            new Times { nome  = "Nautico",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/nautico/feed.rss" },
            new Times { nome  = "Palmeiras",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/palmeiras/feed.rss" },
            new Times { nome  = "Paraná",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/parana/feed.rss" },
            new Times { nome  = "Portuguesa",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/portuguesa/feed.rss" },
            new Times { nome  = "Santa Cruz",
                        link = "http://globoesporte.globo.com/Esportes/Rss/0,,AS0-10073,00.xml" },
            new Times { nome  = "Santos",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/santos/feed.rss" },
            new Times { nome  = "Sport",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/sport/feed.rss" },
            new Times { nome  = "São Paulo",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/sao-paulo/feed.rss" },
            new Times { nome  = "Vasco",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/vasco/feed.rss" },
            new Times { nome  = "Vitória",
                        link = "http://globoesporte.globo.com/servico/semantica/editorias/plantao/futebol/times/vitoria/feed.rss" },
        };


        public List<Times> getTimes()
        {
            return meusTimes;
        }

    }
}