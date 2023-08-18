package com.ecoekb.ui.screens.requestsScreens.requestsList

import androidx.lifecycle.ViewModel
import com.ecoekb.domain.models.Request
import com.ecoekb.domain.usecase.RequestUsecase
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class RequestsListViewModel @Inject() constructor(
    private val requestUsecase: RequestUsecase
) : ViewModel() {

    fun getAllRequest() : List<Request> {
        return requestUsecase.getAllRequest()
    }

    fun filterRequestByStatus(status: String) : List<Request> {
        return getAllRequest().filter { it.status == status }
    }
}