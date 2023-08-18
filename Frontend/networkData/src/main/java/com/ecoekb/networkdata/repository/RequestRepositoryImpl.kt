package com.ecoekb.networkdata.repository

import android.util.Log
import com.ecoekb.domain.models.Request
import com.ecoekb.domain.repository.IRequestRepository

class RequestRepositoryImpl : IRequestRepository {

    val l = mutableListOf<Request>(
        Request(id = "1", status = "Решено"),
        Request(id = "2"),
        Request(id = "3"),
        Request(id = "4"),
        Request(id = "5"),
        Request(id = "6"),
    )

    override fun getRequestById(requestId: String): Request {
        Log.d("REQ REP", requestId)

        Log.d("REQ REP", l.toString())
        return l.filter { it.id == requestId }.first()
    }

    override fun getAllRequest(): List<Request> {
        return l
    }

    override fun addRequest(request: Request) {
        l.add(request)
    }
}