package com.ecoekb.domain.repository

import com.ecoekb.domain.models.Request

interface IRequestRepository {

    fun getRequestById(requestId: String) : Request

    fun getAllRequest() : List<Request>

    fun addRequest(request: Request)
}