package com.ecoekb.ui.screens.requestsScreens.requestItem

import androidx.lifecycle.ViewModel
import com.ecoekb.domain.models.Request
import com.ecoekb.domain.usecase.RequestUsecase
import com.ecoekb.domain.usecase.UserUsecase
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class RequestItemViewModel @Inject() constructor(
    val requestUsecase: RequestUsecase
) : ViewModel() {

    fun getRequestById(requestId: String) : Request {
        return requestUsecase.getRequestById(requestId)
    }
}