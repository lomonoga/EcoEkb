package com.ecoekb.domain.usecase

import com.ecoekb.domain.models.Request
import com.ecoekb.domain.repository.IAuthRepository
import com.ecoekb.domain.repository.IRequestRepository
import javax.inject.Inject

class RequestUsecase @Inject constructor(
    private val requestRepository: IRequestRepository
) {

    fun getRequestById(requestId: String) : Request {
        return requestRepository.getRequestById(requestId)
    }

    fun getAllRequest() : List<Request> {
        return requestRepository.getAllRequest()
    }

    fun addRequest(request: Request) {
        return requestRepository.addRequest(request)
    }
}