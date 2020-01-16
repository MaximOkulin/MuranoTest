using SearchEngine.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Tests.Helpers
{
    public static class TestHelper
    {
        /// <summary>
        /// Returns reference yandex search object
        /// </summary>
        /// <returns></returns>
        public static yandexsearch GetTestYandexSearch()
        {
            return new yandexsearch()
            {
                response = new yandexsearchResponse
                {
                    results = new yandexsearchResponseResults
                    {
                        grouping = new yandexsearchResponseResultsGrouping
                        {
                            group = new yandexsearchResponseResultsGroupingGroup[]
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
                                            url = "https://kazantravel.ru/attractions/kazanskiy-kreml"
                                        }
                                    },
                                    new yandexsearchResponseResultsGroupingGroup
                                    {
                                        doc = new yandexsearchResponseResultsGroupingGroupDoc
                                        {
                                            title = "Казанский кремль — официальная информация с фото",
                                            url = "https://wikiway.com/russia/kazan/kazanskiy-kreml"
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
                                }
                        }
                    }
                }
            };
        }
    }
}
