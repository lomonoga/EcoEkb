package com.ecoekb.ui.screens.onboarding

import com.ecoekb.R

data class OnBoardingItem(
    val title: String,
    val text: String,
    val image: Int,
) {
    companion object {

        fun get() = listOf(
            OnBoardingItem(
                title = "Давайте сделаем наш город лучше",
                text = "Отправляйте сигналы-сообщения о точках загрязнения разных районов.",
                image = R.drawable.love_the_earth_1),
            OnBoardingItem(
                title = "Следите за статусом ваших обращений онлайн",
                text = "В личном кабинете отображаются все ваши активные обращения и события.",
                image = R.drawable.planting_tree_together_2),
            OnBoardingItem(
                title = "Внесите свой вклад в экосистему города",
                text = "Принимайте участие в эко - мероприятиях и приглашайте друзей. ",
                image = R.drawable.artboard_3),
        )
    }
}
