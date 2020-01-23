using SearchEngine.Business.Responses;

namespace SearchEngine.Tests.Helpers
{
    public static class TestHelper
    {
        /// <summary>
        /// Returns test yandex response with query "Саммит большой восьмерки"
        /// </summary>
        /// <returns></returns>
        public static yandexsearch GetTestGroupOfEightSearch()
        {
            var group = new yandexsearchResponseResultsGroupingGroup[]
                              {
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                                title = "Большая семёрка — Википедия",
                                                url = "https://ru.wikipedia.org/wiki/%D0%91%D0%BE%D0%BB%D1%8C%D1%88%D0%B0%D1%8F_%D1%81%D0%B5%D0%BC%D1%91%D1%80%D0%BA%D0%B0"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                           title = "Большая Восьмерка — Wikimedia Foundation",
                                            url = "https://dic.academic.ru/dic.nsf/ruwiki/815924"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                            title = "Группа восьми (G8, Большая восьмерка): история создания...",
                                            url = "https://ria.ru/20130614/943202127.html"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                            title = "Основные саммиты большой восьмерки",
                                            url = "https://www.krugosvet.ru/enc/gumanitarnye_nauki/sociologiya/BOLSHAYA_VOSMERKA.html"
                                        }
                                    }
                              };

            return GetTestYandexSearch(group);
        }

        /// <summary>
        /// Returns test yandex response with query "Казанский Кремль"
        /// </summary>
        /// <returns></returns>
        public static yandexsearch GetTestKazanKremlinSearch()
        {
            var group = new yandexsearchResponseResultsGroupingGroup[]
                                {
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                                title = "В Благовещенском соборе Казанского Кремля состоится...",
                                                url = "https://kazan-kremlin.ru/"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                           title = "Казанский кремль — Википедия",
                                            url = "https://ru.wikipedia.org/wiki/%D0%9A%D0%B0%D0%B7%D0%B0%D0%BD%D1%81%D0%BA%D0%B8%D0%B9_%D0%BA%D1%80%D0%B5%D0%BC%D0%BB%D1%8C"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                            title = "Казанский Кремль - описание, история, фото, музеи, экскурсии",
                                            url = "https://kazantravel.ru/attractions/kazanskiy-kreml/"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                            title = "Казанский кремль — официальная информация с фото",
                                            url = "https://wikiway.com/russia/kazan/kazanskiy-kreml/"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                            title = "Музей-заповедник Казанский Кремль | ВКонтакте",
                                            url = "https://vk.com/kazan_kremlin"
                                        }
                                    }
                                };

            return GetTestYandexSearch(group);

        }
        /// <summary>
        /// Returns reference yandex search object
        /// </summary>
        /// <returns></returns>
        private static yandexsearch GetTestYandexSearch(yandexsearchResponseResultsGroupingGroup[] injectedGroup)
        {
            return new yandexsearch()
            {
                response = new yandexsearchResponse
                {
                    results = new yandexsearchResponseResults
                    {
                        grouping = new yandexsearchResponseResultsGrouping
                        {
                            group = injectedGroup
                        }
                    }
                }
            };
        }
    }
}
