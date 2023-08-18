package com.ecoekb.domain.models

import androidx.compose.runtime.Stable

@Stable
data class Request(
    val id: String = "1",

    val title: String = "Ликвидация свалки",
    val numberRequest: Int = 876,

    val status: String = "В обработке",
    val data: String = "18.01.2023",

    val content: String = "Оформление документов по ликвидации несанкционированных свалок в границах городов и решение сопутствующих задач.",
    val location: String = "Екатеринбург, улица Розы Люксембург 34A"
)

class RequestStatus{
    val ACTIVE = "В обработке"
    val COMPLETE = "Решено"
}