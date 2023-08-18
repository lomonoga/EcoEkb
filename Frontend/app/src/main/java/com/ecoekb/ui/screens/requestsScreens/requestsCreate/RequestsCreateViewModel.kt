package com.ecoekb.ui.screens.requestsScreens.requestsCreate

import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import com.ecoekb.domain.models.Request
import com.ecoekb.domain.usecase.RequestUsecase
import dagger.hilt.android.lifecycle.HiltViewModel
import java.util.Date
import javax.inject.Inject
import kotlin.random.Random
import kotlin.random.nextInt

@HiltViewModel
class RequestsCreateViewModel @Inject() constructor(
    private val usecase: RequestUsecase
) : ViewModel() {
    val address = mutableStateOf("")
    val content = mutableStateOf("")
    val title = mutableStateOf("")

    fun createRequest(){
        usecase.addRequest(
            Request(
                id = Random(Date().time).nextInt().toString(),
                title = title.value,
                numberRequest = Random(Date().time).nextInt(0..999),
                status = "В обработке",
                data = "18.08.2023",
                content = content.value,
                location = address.value
            )
        )
    }
}